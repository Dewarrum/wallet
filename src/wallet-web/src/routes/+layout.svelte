<script lang="ts">
	import { QueryClient, QueryClientProvider } from '@tanstack/svelte-query';
	import '../app.css';
	import TopBar from '../lib/utils/TopBar.svelte';
	import { Modal, initializeStores, type ModalComponent } from '@skeletonlabs/skeleton';
	import CreateTransaction from '../lib/transactions/CreateTransaction.svelte';
	import { computePosition, autoUpdate, offset, shift, flip, arrow } from '@floating-ui/dom';
	import { storePopup } from '@skeletonlabs/skeleton';
	import SideBar from '$lib/utils/SideBar.svelte';

	const queryClient = new QueryClient();
	initializeStores();

	const modalRegistry: Record<string, ModalComponent> = {
		createTransaction: { ref: CreateTransaction }
	};

	storePopup.set({ computePosition, autoUpdate, offset, shift, flip, arrow });
</script>

<QueryClientProvider client={queryClient}>
	<Modal components={modalRegistry} />

	<TopBar />

	<div class="flex">
		<SideBar />

		<main class="container mx-auto">
			<div class="mb-4"></div>
			<slot />
		</main>
	</div>
</QueryClientProvider>
