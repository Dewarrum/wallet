<script>
	import Menu from 'lucide-svelte/icons/menu';
	import CircleUser from 'lucide-svelte/icons/circle-user';
	import Package2 from 'lucide-svelte/icons/package-2';

	import { Button } from '$lib/components/ui/button/index';
	import * as Sheet from '$lib/components/ui/sheet/index';
	import * as DropdownMenu from '$lib/components/ui/dropdown-menu/index';
	import { page } from '$app/stores';
	import { signOut } from '@auth/sveltekit/client';

	let route;
	$: route = $page.url.pathname.split('/')[2];
</script>

<div class="flex min-h-screen w-full flex-col">
	<header class="sticky top-0 flex h-16 items-center gap-4 border-b bg-background px-4 md:px-6">
		<nav
			class="hidden flex-col gap-6 text-lg font-medium md:flex md:flex-row md:items-center md:gap-5 md:text-sm lg:gap-6"
		>
			<a href="/app" class="flex items-center gap-2 text-lg font-semibold md:text-base">
				<Package2 class="h-6 w-6" />
				<span class="sr-only">Wallet</span>
			</a>
			<a
				href="/app/transactions"
				class="transition-colors hover:text-foreground"
				class:text-foreground={route === 'transactions'}
				class:text-muted-foreground={route !== 'transactions'}
			>
				Transactions
			</a>
			<a
				href="/app/profiles"
				class="transition-colors hover:text-foreground"
				class:text-foreground={route === 'profiles'}
				class:text-muted-foreground={route !== 'profiles'}
			>
				Profiles
			</a>
			<a
				href="/app/categories"
				class="transition-colors hover:text-foreground"
				class:text-foreground={route === 'categories'}
				class:text-muted-foreground={route !== 'categories'}
			>
				Categories
			</a>
		</nav>
		<Sheet.Root>
			<Sheet.Trigger asChild let:builder>
				<Button variant="outline" size="icon" class="shrink-0 md:hidden" builders={[builder]}>
					<Menu class="h-5 w-5" />
					<span class="sr-only">Toggle navigation menu</span>
				</Button>
			</Sheet.Trigger>
			<Sheet.Content side="left">
				<nav class="grid gap-6 text-lg font-medium">
					<a href="##" class="flex items-center gap-2 text-lg font-semibold">
						<Package2 class="h-6 w-6" />
						<span class="sr-only">Acme Inc</span>
					</a>
					<a href="##" class="hover:text-foreground"> Transactions </a>
					<a href="##" class="text-muted-foreground hover:text-foreground"> Profiles </a>
					<a href="##" class="text-muted-foreground hover:text-foreground"> Categories </a>
				</nav>
			</Sheet.Content>
		</Sheet.Root>
		<div class="flex w-full items-center gap-4 md:ml-auto md:gap-2 lg:gap-4">
			<span class="flex-1"></span>
			<DropdownMenu.Root>
				<DropdownMenu.Trigger asChild let:builder>
					<Button builders={[builder]} variant="secondary" size="icon" class="rounded-full">
						<CircleUser class="h-5 w-5" />
						<span class="sr-only">Toggle user menu</span>
					</Button>
				</DropdownMenu.Trigger>
				<DropdownMenu.Content align="end">
					<DropdownMenu.Label>My Account</DropdownMenu.Label>
					<DropdownMenu.Separator />
					<DropdownMenu.Item>Settings</DropdownMenu.Item>
					<DropdownMenu.Separator />
					<DropdownMenu.Item
						on:click={() =>
							signOut({
								callbackUrl: '/'
							})}>Logout</DropdownMenu.Item
					>
				</DropdownMenu.Content>
			</DropdownMenu.Root>
		</div>
	</header>
	<slot />
</div>
