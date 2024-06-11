<script lang="ts">
	import { Button } from '$lib/components/ui/button/index';
	import * as Card from '$lib/components/ui/card/index';
	import ArrowUpRight from 'lucide-svelte/icons/arrow-up-right';
	import TransactionTable from '$lib/components/transactions/transaction-table.svelte';
	import * as Select from '$lib/components/ui/select';

	export let data;
	const { transactions, profiles } = data;
</script>

<Card.Root class="xl:col-span-2">
	<Card.Header class="flex flex-row items-center">
		<div class="grid gap-4 lg:w-1/3">
			<Card.Title>Transactions</Card.Title>

			<Select.Root>
				<Select.Trigger class="w-[180px]">
					<Select.Value placeholder={profiles.at(0)?.id} />
				</Select.Trigger>
				<Select.Content>
					{#each profiles as profile}
						<Select.Item value={profile.id}>{profile.id}</Select.Item>
					{/each}
				</Select.Content>
			</Select.Root>
		</div>
		<Button href="##" size="sm" class="ml-auto gap-1">
			View All
			<ArrowUpRight class="h-4 w-4" />
		</Button>
	</Card.Header>
	<Card.Content>
		<TransactionTable {transactions} />
	</Card.Content>
</Card.Root>
